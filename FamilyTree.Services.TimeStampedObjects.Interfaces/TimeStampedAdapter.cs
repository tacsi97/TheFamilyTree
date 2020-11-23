using System;

namespace FamilyTree.Services.TimeStampedObjects.Interfaces
{
    public abstract class TimeStampedAdapter<T> : ITimeStampedObject
    {
        public T TemplateObject { get; set; }
        public abstract string PrimaryData { get; set; }
        public abstract string Description { get; set; }
        public abstract DateTime Time { get; set; }

        public TimeStampedAdapter(T templateObject)
        {
            TemplateObject = templateObject;
        }
    }
}
