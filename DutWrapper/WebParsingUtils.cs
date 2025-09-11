using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static class WebParsingUtils
    {
        public static class WebTable
        {
            public class WebTableHeader
            {
                public int Index { get; private set; }

                public string? Name { get; private set; }

                public string Hash { get; private set; }

                public List<WebTableHeader> SubHeaderItems { get; private set; }

                public WebTableHeader? GetHeaderByName(string headerName, bool trimBeforeSearch = true)
                {
                    return SubHeaderItems.FirstOrDefault(x =>
                    {
                        string? final1 = trimBeforeSearch ? x.Name?.Trim() : x.Name;
                        return final1?.CompareTo(headerName) == 0;
                    });
                }

                public bool IsEqualValue(WebTableHeader header)
                {
                    return Index == header.Index && Name?.CompareTo(header.Name) == 0 && Hash.CompareTo(header.Hash) == 0;
                }

                public WebTableHeader(int index, string? name = null, List<WebTableHeader>? subItem = null)
                {
                    this.Index = index;
                    this.Name = name;
                    this.SubHeaderItems = new List<WebTableHeader>();
                    if (subItem != null)
                    {
                        this.SubHeaderItems.AddRange(subItem);
                    }
                    this.Hash = FunctionExtension.RandomString(32);
                }
            }

            public class WebTableCell
            {
                public WebTableCell(int index, string? value, List<string>? classList = null)
                {
                    Index = index;
                    Value = value;
                    ClassList = new List<string>();
                    if (classList != null)
                    {
                        ClassList.AddRange(classList);
                    }
                }

                public int Index { get; private set; }

                public string? Value { get; private set; }

                public List<string> ClassList { get; private set; }

                public float? FloatValue
                {
                    get
                    {
                        if (Value == null)
                        {
                            return null;
                        }

                        if (float.TryParse(Value, out var value1))
                        {
                            return value1;
                        }
                        return null;
                    }
                }

                public int? IntValue
                {
                    get
                    {
                        if (Value == null)
                        {
                            return null;
                        }

                        if (int.TryParse(Value, out var value1))
                        {
                            return value1;
                        }
                        return null;
                    }
                }

                public long? LongValue
                {
                    get
                    {
                        if (Value == null)
                        {
                            return null;
                        }

                        if (long.TryParse(Value, out var value1))
                        {
                            return value1;
                        }
                        return null;
                    }
                }

                public double? DoubleValue
                {
                    get
                    {
                        if (Value == null)
                        {
                            return null;
                        }

                        if (double.TryParse(Value, out var value1))
                        {
                            return value1;
                        }
                        return null;
                    }
                }

                public bool? BoolValue
                {
                    get
                    {
                        if (Value == null)
                        {
                            return null;
                        }

                        if (bool.TryParse(Value, out var value1))
                        {
                            return value1;
                        }
                        return null;
                    }
                }

                public bool BoolCellCheck
                {
                    get
                    {
                        return ClassList.Contains("GridCheck");
                    }
                }
            }

            public class WebTableRow
            {
                public List<WebTableCell> CellList { get; private set; }

                public WebTableCell this[int index]
                {
                    get
                    {
                        return CellList[index];
                    }
                }

                public WebTableRow(List<WebTableCell>? cellList = null)
                {
                    CellList = new List<WebTableCell>();
                    if (cellList != null)
                    {
                        CellList.AddRange(cellList);
                    }
                }
            }

            public class WebTableBody
            {
                public List<WebTableHeader> Headers { get; private set; }

                public List<WebTableRow> Rows { get; private set; }

                public int HeaderColumnCount
                {
                    get
                    {
                        int CalcCount(List<WebTableHeader> list)
                        {
                            return list.Sum(p =>
                            {
                                if (p.SubHeaderItems.Count == 0)
                                {
                                    return 1;
                                }
                                else
                                {
                                    return CalcCount(p.SubHeaderItems);
                                }
                            });
                        }

                        return CalcCount(Headers);
                    }
                }

                public int HeaderIndexOf(WebTableHeader? header)
                {
                    if (header == null)
                    {
                        return -1;
                    }

                    int GetIndex(WebTableHeader header, List<WebTableHeader> source, bool keepPrevious = false)
                    {
                        int index = 0;

                        foreach (var item in source)
                        {
                            if (item.IsEqualValue(header))
                            {
                                return index;
                            }
                            else if (item.SubHeaderItems.Count > 0)
                            {
                                var indexSub = GetIndex(header, item.SubHeaderItems, true);
                                index += indexSub;

                                if (indexSub != item.SubHeaderItems.Count)
                                {
                                    return index;
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }

                        return keepPrevious ? index : -1;
                    }

                    return GetIndex(header, Headers);
                }

                public WebTableHeader? GetFirstHeaderByName(string headerName, bool trimBeforeSearch = true)
                {
                    return Headers.FirstOrDefault(x =>
                    {
                        string? final1 = trimBeforeSearch ? x.Name?.Trim() : x.Name;
                        return final1?.CompareTo(headerName) == 0;
                    });
                }

                public WebTableCell? GetValueFromHeader(int rowIndex, WebTableHeader? header)
                {
                    if (header == null)
                    {
                        return null;
                    }

                    int headerIndex = HeaderIndexOf(header);
                    if (headerIndex == -1)
                    {
                        return null;
                    }
                    else if (Headers.Count -1 < headerIndex)
                    {
                        return null;
                    }

                    if (Rows.Count - 1 < rowIndex || rowIndex < 0)
                    {
                        return null;
                    }

                    return Rows[rowIndex][headerIndex];
                }

                public WebTableBody(List<WebTableHeader> headers, List<WebTableRow> rows)
                {
                    Headers = headers;
                    Rows = rows;
                }
            }

            public static WebTableBody ParseTableTag(IElement tableElement)
            {
                // Define
                List<WebTableHeader> Headers = new List<WebTableHeader>();
                List<WebTableRow> Rows = new List<WebTableRow>();

                // Parsing header
                var headerList = tableElement.GetElementsByClassName("GridHeader").ToList();
                var headerListParent = new List<WebTableHeader>();
                for (int i = headerList.Count - 1; i >= 0; i--)
                {
                    var headerListChild = new List<WebTableHeader>();

                    var headerCellList = headerList[i].GetElementsByClassName("GridHeaderCell").ToList();
                    for (int j = 0; j < headerCellList.Count; j++)
                    {
                        var headerCell = headerCellList[j];

                        var index = j;
                        var name = headerCell.TextContent;
                        var headerCellItem = new WebTableHeader(index, name, null);

                        var colCount = 0;
                        int.TryParse(headerCell.GetAttribute("colspan"), out colCount);

                        while (colCount > 0 && headerListParent.Count > 0)
                        {
                            headerCellItem.SubHeaderItems.Add(headerListParent[0]);
                            headerListParent.RemoveAt(0);

                            colCount -= 1;
                        }

                        headerListChild.Add(headerCellItem);
                    }

                    headerListParent.Clear();
                    headerListParent.AddRange(headerListChild);
                    headerListChild.Clear();
                }
                Headers.AddRange(headerListParent);
                headerListParent.Clear();

                // Parsing row
                foreach (var webRow in tableElement.GetElementsByClassName("GridRow").ToList())
                {
                    WebTableRow row = new WebTableRow();

                    var webCellList = webRow.GetElementsByClassName("GridCell").ToList();
                    for (int i = 0; i < webCellList.Count; i++)
                    {
                        WebTableCell cell = new WebTableCell(
                            i,
                            webCellList[i].TextContent,
                            webCellList[i].ClassList.ToList()
                            );
                        row.CellList.Add(cell);
                    }

                    Rows.Add(row);
                }

                // Return
                return new WebTableBody(Headers, Rows);
            }
        }

        public static async Task<IDocument> AngleSharpHtmlToDocument(string html)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            return await context.OpenAsync(req => req.Content(html));
        }
    }
}
