namespace ServiseBot.Models.Caches
{
    internal class TimeOfCache : UserCache
    {
        public override string Data { get; set; }
        public string Operation { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Substantiation { get; set; }
        public bool Decision { get; set; }
        public override void ClearData()
        {

        }
    }
}
