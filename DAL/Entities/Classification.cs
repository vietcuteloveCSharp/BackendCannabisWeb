namespace DAL.Entities
{
    public class Classification : BaseEntity
    {
       
        [Key]
        public int Id { get; set; }
		public ESeedClassify Type { get; set; }
		public string? Description { get; set; }
        public bool IsActive {  get; set; } =true;
        //navigation

        public virtual ICollection<Seed> Seeds { get; set; }  = new HashSet<Seed>();
	}
}
