using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class RequestLog
    {
        [Key]
        public int RequestLogSN { get; set; }

        public DateTime LogTime { get; set; }

        [MaxLength(30)]
        public string IP { get; set; }

        [MaxLength(30)]
        public string Host { get; set; }

        public string browser { get; set; }

        [MaxLength(30)]
        public string requestType { get; set; }

        [MaxLength(30)]
        public string userHostAddress { get; set; }

        [MaxLength(30)]
        public string userHostName { get; set; }

        [MaxLength(30)]
        public string httpMethod { get; set; }
    }
}