namespace MVCPages.SampleWeb
{
    [MVCPages.PageUrl("/news")]
    public class news : Models.PageViewModel
    {
        public news()
        {
            BodyText = "Lorem";
            Header = "News";
            Introduction = "Lorem ipsum";
        }
    }
}