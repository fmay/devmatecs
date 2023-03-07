using System;

namespace Crotchety.Domain
{

    public class User
    {
        public string uid = "";
        public UserType userType = UserType.PERSON;
        public bool hasAvatar = false;
        public bool hasBanner = false;
        public Skill[] skills = { };
        public Profile profile = new Profile();
    }


}

