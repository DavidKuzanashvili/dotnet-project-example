namespace App.Domain.Entities.Languages
{
    public abstract class BaseTranslation
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public Language Language { get; set; }
    }
}
