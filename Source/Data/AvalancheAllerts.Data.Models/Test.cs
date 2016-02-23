using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalancheAllerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Device.Location;

    using AvalancheAllerts.Data.Common.Models;

    public class Test : BaseModel<int>
    {
        /*modelBuilder.Entity<Test>().Property(m => m.Position.Latitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Latitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Longitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Altitude).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Course).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.HorizontalAccuracy).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.Speed).IsOptional();
            modelBuilder.Entity<Test>().Property(m => m.Position.VerticalAccuracy).IsOptional();*/

        public Test()
        {
            this.Organisations = new HashSet<Organisation>();
        }

        
        protected double? Latitude { get; set; }

        
        protected double? Longitude { get; set; }

        
        protected double? Altitude { get; set; }

        
        protected double? Course { get; set; }

        
        protected double? HorizontalAccuracy { get; set; }

        
        protected double? Speed { get; set; }

        
        protected double? VerticalAccuracy { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Place { get; set; }

        [NotMapped]
        public virtual GeoCoordinate Position {
            get
            {
                double latitude;
                if (this.Latitude == null)
                {
                    latitude = double.NaN;
                }
                else
                {
                    latitude = this.Latitude.Value;
                }

                double longitude;
                if (this.Longitude == null)
                {
                    longitude = double.NaN;
                }
                else
                {
                    longitude = this.Longitude.Value;
                }

                double altitude;
                if (this.Altitude == null)
                {
                    altitude = double.NaN;
                }
                else
                {
                    altitude = this.Altitude.Value;
                }

                double horizontalAccuracy;
                if (this.HorizontalAccuracy == null)
                {
                    horizontalAccuracy = double.NaN;
                }
                else
                {
                    horizontalAccuracy = this.HorizontalAccuracy.Value;
                }

                double verticalAccuracy;
                if (this.VerticalAccuracy == null)
                {
                    verticalAccuracy = double.NaN;
                }
                else
                {
                    verticalAccuracy = this.VerticalAccuracy.Value;
                }

                double speed;
                if (this.Speed == null)
                {
                    speed = double.NaN;
                }
                else
                {
                    speed = this.Speed.Value;
                }

                double course;
                if (this.Course == null)
                {
                    course = double.NaN;
                }
                else
                {
                    course = this.Course.Value;
                }

                return new GeoCoordinate(latitude, longitude, altitude, horizontalAccuracy, verticalAccuracy, speed, course);
            }

            set
            {
                if (double.IsNaN(value.Latitude))
                {
                    this.Latitude = null;
                }
                else
                {
                    this.Latitude = value.Latitude;
                }

                if (double.IsNaN(value.Longitude))
                {
                    this.Longitude = null;
                }
                else
                {
                    this.Longitude = value.Longitude;
                }

                if (double.IsNaN(value.Altitude))
                {
                    this.Altitude = null;
                }
                else
                {
                    this.Altitude = value.Altitude;
                }

                if (double.IsNaN(value.Course))
                {
                    this.Course = null;
                }
                else
                {
                    this.Course = value.Course;
                }

                if (double.IsNaN(value.HorizontalAccuracy))
                {
                    this.HorizontalAccuracy = null;
                }
                else
                {
                    this.HorizontalAccuracy = value.HorizontalAccuracy;
                }

                if (double.IsNaN(value.Speed))
                {
                    this.Speed = null;
                }
                else
                {
                    this.Speed = value.Speed;
                }

                if (double.IsNaN(value.VerticalAccuracy))
                {
                    this.VerticalAccuracy = null;
                }
                else
                {
                    this.VerticalAccuracy = value.VerticalAccuracy;
                }
            }
        }

        [Required]
        public string TestResultsDescription { get; set; }

        [Range(1, 5)]
        public int DangerLevel { get; set; }

        public string UserId { get; set; }
        
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }

        public class Testfiguration : EntityTypeConfiguration<Test>
        {
            public Testfiguration()
            {
                this.Property(p => p.Latitude);
                this.Property(p => p.Longitude);
                this.Property(p => p.Altitude);
                this.Property(p => p.Course);
                this.Property(p => p.Speed);
                this.Property(p => p.HorizontalAccuracy);
                this.Property(p => p.VerticalAccuracy);
            }
        }
    }
}
