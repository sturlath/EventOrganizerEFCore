namespace EventOrganizer.Permissions
{
    public static class EventOrganizerPermissions
    {
        public const string GroupName = "EventOrganizer";


        //TODO:
        public static class Events
        {
            public const string Default = GroupName + ".Events";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}