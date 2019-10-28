using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class MetaAdministrator
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(10, ErrorMessage = "帳號不可超過10碼")]
        public string admAcc { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string admPass { get; set; }
        [DisplayName("暱稱")]
        [Required(ErrorMessage = "請輸入暱稱")]
        public string admNick { get; set; }
    }

    public class MetaAcc_Pass
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(10, ErrorMessage = "帳號不可超過10碼")]
        public string Account { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }

    public class MetaJoin_Fun_Activities
    {
        [DisplayName("活動ID")]
        public string actId { get; set; }
        [DisplayName("主辦人會員ID")]
        public string hostId { get; set; }
        [DisplayName("活動主題")]
        public string actTopic { get; set; }
        [DisplayName("活動舉辦時間")]
        public System.DateTime actTime { get; set; }
        [DisplayName("活動說明")]
        public string actDescription { get; set; }
        [DisplayName("年齡限制")]
        public string ageRestrict { get; set; }
        [DisplayName("性別限制")]
        public string gender { get; set; }
        [DisplayName("人數限制")]
        public short maxNumPeople { get; set; }
        [DisplayName("預算")]
        public decimal maxBudget { get; set; }
        [DisplayName("縣/市")]
        public short actCounty { get; set; }
        [DisplayName("鄉/鎮/市/區")]
        public short actDistrict { get; set; }
        [DisplayName("路名")]
        public string actRoad { get; set; }
        [DisplayName("付款方式")]
        public string paymentTerm { get; set; }
        [DisplayName("是否保留此揪團活動")]
        public bool keepAct { get; set; }
        [DisplayName("是否可退出")]
        public bool acceptDrop { get; set; }
        [DisplayName("點擊次數")]
        public int clickTimes { get; set; }
        [DisplayName("報名截止時間")]
        public System.DateTime actDeadline { get; set; }
        [DisplayName("活動類別ID")]
        public string actClassId { get; set; }
    }

    public class MetaCounty
    {
        [DisplayName("縣/市")]
        public string CountyName { get; set; }
    }

    public class MetaDistrict
    {
        [DisplayName("鄉/鎮/市/區")]
        public string DistrictName { get; set; }
    }

    public class MetaMember
    {
        [Key]
        [StringLength(10)]
        [DisplayName("會員ID")]
        public string memId { get; set; }

        [Required(ErrorMessage = "請輸入暱稱")]
        [StringLength(15, ErrorMessage = "暱稱不可超過15碼")]
        [DisplayName("暱稱")]
        public string memNick { get; set; }

        [Required(ErrorMessage = "請輸入您的郵件信箱")]
        [StringLength(50)]
        [DisplayName("電子郵件信箱")]
        public string Email { get; set; }

        [StringLength(24)]
        [DisplayName("手機號碼")]
        public string cellPhone { get; set; }

        [DisplayName("縣/市")]
        public short memCounty { get; set; }

        [DisplayName("鄉/鎮/市/區")]
        public short memDistrict { get; set; }

        [DisplayName("註冊時間")]
        public DateTime timeReg { get; set; }

        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        [DisplayName("違規次數")]
        public short numViolate { get; set; }

        [DisplayName("停權")]
        public bool Suspend { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "請輸入\"男\"或\"女\"")]
        [DisplayName("性別")]
        public string Sex { get; set; }

        [DisplayName("是否已審核")]
        public bool Approved { get; set; }

        [DisplayName("個人簡介")]
        public string Introduction { get; set; }
    }
}