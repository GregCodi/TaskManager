namespace TaskManager.API.Models
{
    public class CommonObject
    {

        public int Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[]? Photo { get; set; }
        public CommonObject()
        {
            CreationDate = DateTime.Now;
        }

    }
}
