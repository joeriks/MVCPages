namespace MVCPages.SampleWeb
{
    [MVCPages.PageUrl("/news/old-software","Alternative")]
    public class old_software : Models.PageViewModel
    {
        public old_software()
        {
            Title = "OLD";
            BodyText = "Lorem";
            Header = "OLD Software";
            Introduction = "Lorem ipsum";
        }
    }
}
