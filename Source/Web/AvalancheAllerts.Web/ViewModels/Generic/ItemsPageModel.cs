namespace AvalancheAllerts.Web.ViewModels.Generic
{
    using System.Collections.Generic;

    public class ItemsPageModel<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}