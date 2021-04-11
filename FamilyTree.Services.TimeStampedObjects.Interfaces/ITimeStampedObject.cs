using System;

namespace FamilyTree.Services.TimeStampedObjects.Interfaces
{
    public interface ITimeStampedObject
    {
        string PrimaryData { get; set; }

        string Description { get; set; }

        DateTime Time { get; set; }
    }
}
