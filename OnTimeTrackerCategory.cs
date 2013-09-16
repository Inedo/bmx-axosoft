using System;
using System.Collections.Generic;
using Inedo.BuildMaster.Extensibility.Providers.IssueTracking;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    [Serializable]
    public sealed class OnTimeTrackerCategory : IssueTrackerCategory
    {
        public OnTimeTrackerCategory(string categoryId, string categoryName, IEnumerable<OnTimeTrackerCategory> subCategories)
            : base(categoryId, categoryName, ToArray(subCategories))
        {
        }

        private static IssueTrackerCategory[] ToArray(IEnumerable<OnTimeTrackerCategory> categories)
        {
            if (categories == null)
                return new IssueTrackerCategory[0];

            return new List<OnTimeTrackerCategory>(categories).ToArray();
        }
    }
}
