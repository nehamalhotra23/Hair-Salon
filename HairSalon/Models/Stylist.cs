namespace HairSalon.Models
{
  public class Stylist
  {
      public Stylist()
        {
            this.Client = new HashSet<Client>();
        }

        public int StylistId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
  }
