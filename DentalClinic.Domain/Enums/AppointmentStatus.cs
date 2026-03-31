namespace DentalClinic.Domain.Enums;

public enum AppointmentStatus
{
    Scheduled,      // booked, waiting for the day
    Confirmed,      // patient confirmed they're coming
    InProgress,     // patient is currently in the chair
    Completed,      // treatment done
    Cancelled,      // cancelled by patient or clinic
    NoShow,         // patient didn't show up
    Rescheduled     // moved to a different time
}