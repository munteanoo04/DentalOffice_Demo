namespace DentalClinic.Application.Contracts.DTOs;

public record StatisticsDto(
    int TotalAppointments,
    int Scheduled,
    int Completed,
    int Cancelled,
    List<DoctorStatDto> ByDoctor,
    List<ProcedureStatDto> ByProcedure
);

public record DoctorStatDto(
    string DoctorName,
    int Total,
    int Completed,
    int Cancelled
);

public record ProcedureStatDto(
    string ProcedureName,
    int Total,
    decimal Revenue
);