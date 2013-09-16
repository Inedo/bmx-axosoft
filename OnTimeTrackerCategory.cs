using System;
using System.Collections.Generic;
using System.Text;
using Inedo.BuildMaster.Extensibility.Providers.IssueTracking;
using System.Data;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    [Serializable]
    public sealed class OnTimeTrackerCategory : CategoryBase
    {
        public OnTimeTrackerCategory(string categoryId, string categoryName, IEnumerable<OnTimeTrackerCategory> subCategories)
            : base(categoryId, categoryName, ToArray(subCategories))
        {
        }

        private static CategoryBase[] ToArray(IEnumerable<OnTimeTrackerCategory> categories)
        {
            if (categories == null)
                return new CategoryBase[0];

            return new List<OnTimeTrackerCategory>(categories).ToArray();
        }
    }
}
