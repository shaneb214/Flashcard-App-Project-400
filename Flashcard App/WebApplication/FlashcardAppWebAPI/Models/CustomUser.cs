using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FlashcardAppWebAPI.Models
{
    public class CustomUser
    {
        [Key,ForeignKey("User")]
        public string ID { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        //public virtual ICollection<LanguageProfile> LanguageProfiles { get; set; }
    }

    public class Language
    { 
        [Key]
        public string ISO { get; set; }
        public string Name { get; set; }  
    }

    public class LanguageProfile
    {
        [Key]
        public string ID { get; set; }

        [ForeignKey("User")]
        public string userID { get; set; }
        [JsonIgnore]
        public virtual CustomUser User { get; set; }


        //[ForeignKey("DefaultSet")]
        //public string defaultSetID { get; set; }
        //[JsonIgnore]
        //public virtual Set DefaultSet { get; set; }

        //public virtual ICollection<Set> Sets { get; set; }


        [ForeignKey("NativeLanguage")]
        public string nativeLanguageISO { get; set; }   
        public virtual Language NativeLanguage { get; set; }

        [ForeignKey("LearningLanguage")]
        public string learningLanguageISO { get; set; }
        public virtual Language LearningLanguage { get; set; }

        public bool IsCurrentProfile { get; set; }
    }

    public class Set
    {
        [Key]
        public string ID { get; set; }

        public string Name { get; set; }

        [ForeignKey("LanguageProfile")]
        public string LanguageProfileID { get; set; }
        [JsonIgnore]
        public virtual LanguageProfile LanguageProfile { get; set; }

        [ForeignKey("ParentSet")]
        public string parentSetID { get; set; }
        [JsonIgnore]
        public virtual Set ParentSet { get; set; }

        public bool IsDefaultSet { get; set; }
    }

    public class Flashcard
    {
        [Key]
        public string ID { get; set; }


        [ForeignKey("Set")]
        public string setID { get; set; }
        [JsonIgnore]
        public virtual Set Set { get; set; }

        public string nativeSide { get; set; }
        public string learningSide { get; set; }
        public string notes { get; set; }
    }
}