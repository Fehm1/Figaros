﻿using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Setting : EntityBase, IEntity
    {
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string FooterDescription { get; set; }

        public string OurService { get; set; }
        public string OurServiceDescription { get; set; }

        public string OurPrice { get; set; }
        public string OurPriceDescription { get; set; }

        public string OurBarber { get; set; }
        public string OurBarberDescription { get; set; }

        public string MondayFridayWorkHours { get; set; }
        public string SaturdayWorkHours { get; set; }
        public string SundayWorkHours { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        public string InstagramUrl { get; set; }
        public string WhatsAppUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TiktokUrl { get; set; }

        public bool IsActiceRequest { get; set; }
    }
}
