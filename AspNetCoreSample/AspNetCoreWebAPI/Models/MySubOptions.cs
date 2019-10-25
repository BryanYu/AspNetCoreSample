namespace AspNetCoreWebAPI.Models
{
    public class MySubOptions  
    {
        public MySubOptions()
        {
            SubOption1 = "value1_from_ctor";
        }

        public string SubOption1 { get; set; }

        public int SubOption2 { get; set; }
    }
}