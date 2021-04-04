using System;
using System.Threading.Tasks;

namespace FamilyTree.Core.Extensions
{
    public static class TaskUtilities
    {
        //TODO: add an errorhandler
        public static async void FireAndForgetAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
