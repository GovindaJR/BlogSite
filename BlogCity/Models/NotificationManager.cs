namespace BlogCity.Models
{
    public interface NotificationManager
    {
        public void registerRecipient(Recipient recipient);
        public void removeRecipient(Recipient recipient);

        public void notifyRecipients(Post post);
    }
}
