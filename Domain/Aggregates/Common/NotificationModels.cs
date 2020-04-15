using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.Common
{
    public class Notification
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationId { get; set; }
        public int? Type { get; set; }
        public string Text { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public int? NotifiedBy { get; set; }
        public int? NotifiedTo { get; set; }
        public DateTime NotifiedOn { get; set; }
        public DateTime? NotificationViewOn { get; set; }
        public int? NotificationUserType { get; set; }
    }

    public class NotficiationView
    {
        public long NotificationId { get; set; }
        public int? NotificationType { get; set; }
        public string Text { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string NotificationDuration { get; set; }
    }
}