using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class CheckOutTrackList
    {
        public int Id { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int CheckInTrackListId { get; set; }
        [ForeignKey("CheckInTrackListId")]
        public CheckInTrackList CheckInTrackList {get; set; }


    }
}
