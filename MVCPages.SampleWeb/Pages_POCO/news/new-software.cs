namespace MVCPages.SampleWeb
{
    [MVCPages.PageUrl("/news/new-software")]
    public class new_software : Models.PageViewModel
    {
        public new_software()
        {
            BodyText = "Lorem";
            Header = "New Software";
            Introduction = "Lorem ipsum";
        }
    }
}
