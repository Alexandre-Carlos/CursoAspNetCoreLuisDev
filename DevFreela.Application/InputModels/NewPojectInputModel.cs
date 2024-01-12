namespace DevFreela.Application.InputModels
{
    public class NewPojectInputModel
    {
        public string Title { get; set; }
        public string Descripption { get; set; }
        public int IdClient { get; set; }
        public int IdFreelacer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
