namespace ClinicManager.Core.Entities
{
    public class Service : BaseEntity
    {
        public Service() { }
        public Service(string name, decimal value, int duration)
        {
            Name = name;
            Value = value;
            Duration = duration;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Value { get; set; }
        private int _duration;

        //Duração em mminutos
        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A duração deve ser positiva.");
                }
                _duration = value;
            }
        }

        public ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();
    }
}
