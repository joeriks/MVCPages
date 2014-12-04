namespace MVCPages.SampleWeb
{
    [MVCPages.PageUrl("/")]
    public class index : Models.PageViewModel
    {
        public index()
        {
            BodyText = "Lorem";
            Header = "Index";
            Introduction = "Lorem ipsum";
        }
    }
}
