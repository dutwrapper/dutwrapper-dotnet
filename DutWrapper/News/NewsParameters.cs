using System;

namespace DutWrapper.News
{
    public static class NewsParameters
    {
        public enum NewsType
        {
            /// <summary>
            /// Notifications from Office of Training
            /// </summary>
            Global,

            /// <summary>
            /// Lecturer's announcements to students of the course such as makeup, leaving,...
            /// </summary>
            Subject,

            /// <summary>
            /// News about student affairs room (ex. training assessment, scholarship consideration, tuition exemption, class schedule)
            /// </summary>
            StudentAffairs,

            /// <summary>
            /// News about examination (ex. examination schedules, review, and student opinion surveys)
            /// </summary>
            Examination,

            /// <summary>
            /// Notifications about tuition fee.
            /// </summary>
            TuitionFee,

            /// <summary>
            /// Notifications about statute and regulation.
            /// </summary>
            StatuteRegulation,
        }

        public static int ToValue(this NewsType type)
        {
            switch (type)
            {
                case NewsType.Global: return 0;
                case NewsType.Subject: return 1;
                case NewsType.StudentAffairs: return 2;
                case NewsType.Examination: return 3;
                case NewsType.TuitionFee: return 4;
                case NewsType.StatuteRegulation: return 5;
                default: return -1;
            }
        }

        public enum SearchMethod
        {
            /// <summary>
            /// Search in news title.
            /// </summary>
            ByTitle,

            /// <summary>
            /// Search in news content.
            /// </summary>
            ByContent,
        }

        public enum SubjectStatus
        {
            /// <summary>
            /// Unknown status for this subject.
            /// </summary>
            Unknown = -1,
            /// <summary>
            /// This subject is only send notify to students and won't be changed.
            /// </summary>
            Notify = 0,
            /// <summary>
            /// This subject can't be performed as scheduled and will be make-up lesson later.
            /// </summary>
            Leaving = 1,
            /// <summary>
            /// This subject scheduled a lesson for previous leaving lesson.
            /// </summary>
            MakeUpLesson = 2
        }
    }
}
