namespace AngularBlogCore.API.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Hiç bir zaman API tarafında kural şudur Entity'nin kendisini dönmezsiniz. Mutlaka onu kapsulleyecek ona karşılık gelecek nesne dönmesi gerekiyor.
    }
}