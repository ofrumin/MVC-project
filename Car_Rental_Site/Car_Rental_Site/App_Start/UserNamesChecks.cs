using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace Car_Rental_Site
{
    public static class UsernamesChecks
    {
        public const string READER_WRITER_LOCK_KEY = "usernamesChecksReaderWriterLocker";
        public const string RESERVED_USERNAMES_COLLECTION_KEY = "reservedUsernameCollection";
        public const string RESERVED_USERNAME_INSESSION_KEY = "reservedUsername";

        private static void CreateUsernamesChecksReaderWriterLocker(HttpApplicationStateBase application)
        {
            ReaderWriterLock usernamesChecksReaderWriterLocker = new ReaderWriterLock();
            application[READER_WRITER_LOCK_KEY] = usernamesChecksReaderWriterLocker;
        }

        private static void CreateReservedUsernameCollection(HttpApplicationStateBase application)
        {
            HashSet<string> reservedUsernames = new HashSet<string>();
            application[RESERVED_USERNAMES_COLLECTION_KEY] = reservedUsernames;
        }

        public static void Init(HttpApplicationStateBase application)
        {
            CreateUsernamesChecksReaderWriterLocker(application);
            CreateReservedUsernameCollection(application);
        }

        public static ReaderWriterLock GetUsernamesChecksReaderWriterLocker(HttpApplicationStateBase application)
        {
            return (ReaderWriterLock)application[READER_WRITER_LOCK_KEY];
        }

        public static IEnumerable<string> GetReservedUsernamesCollection(HttpApplicationStateBase application)
        {
            return (IEnumerable<string>)application[RESERVED_USERNAMES_COLLECTION_KEY];
        }

        public static bool IsUsernameReserved(HttpApplicationStateBase application, HttpSessionStateBase session, string username)
        {
            return ((HashSet<string>)application[RESERVED_USERNAMES_COLLECTION_KEY]).Contains(username.ToLower())
                    && (session[RESERVED_USERNAME_INSESSION_KEY] as string) != username;
        }

        public static void StoreUsernameInReservedUsernamesCollection(HttpApplicationStateBase application, HttpSessionStateBase session, string username)
        {
            if (IsUsernameReserved(application, session, username))
            {
                throw new InvalidOperationException("This username is already reserved");
            }
            ((HashSet<string>)application[RESERVED_USERNAMES_COLLECTION_KEY]).Add(username);
            session[RESERVED_USERNAME_INSESSION_KEY] = username;
        }

        public static void ReleaseUsernameFromReservedUsernamesCollection(HttpApplicationStateBase application, string username)
        {
            ReleaseUsernameFromReservedUsernamesCollection(application, null, username);
        }

        public static void ReleaseUsernameFromReservedUsernamesCollection(HttpApplicationStateBase application, HttpSessionStateBase session, string username)
        {
            if (session != null)
            {
                session[RESERVED_USERNAME_INSESSION_KEY] = null;
            }
            ((HashSet<string>)application[RESERVED_USERNAMES_COLLECTION_KEY]).Remove(username);
        }
    }
}