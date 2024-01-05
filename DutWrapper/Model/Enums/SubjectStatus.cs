namespace DutWrapper.Model.Enums
{
    public enum SubjectStatus
    {
        /// <summary>
        /// Unknown status for this subject.
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// This subject is only send notify to students and won't be changed.
        /// </summary>
        Notify,
        /// <summary>
        /// This subject can't be performed as scheduled and will be make-up lesson later.
        /// </summary>
        Leaving,
        /// <summary>
        /// This subject scheduled a lesson for previous leaving lesson.
        /// </summary>
        MakeUpLesson
    }
}
