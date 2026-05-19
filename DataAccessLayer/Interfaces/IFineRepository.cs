namespace DataAccessLayer.Interfaces;

using Models;

public interface IFineRepository
{
    Fine AddFine(Fine fine);

    Fine? GetFineById(int fineId);

    List<Fine> GetPendingFinesByMember(int memberId);

    decimal GetPendingFineAmount(int memberId);

    Fine UpdateFine(Fine fine);

    FinePayment AddFinePayment(FinePayment payment);

    List<FinePayment> GetFinePaymentHistory(int fineId);
}