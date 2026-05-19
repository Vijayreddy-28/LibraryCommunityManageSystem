using DataAccessLayer.Repositories;
using Models;

namespace BusinessLayer.Services;

public class FineService
{
    private readonly FineRepository _fineRepository;

    public FineService()
    {
        _fineRepository = new FineRepository();
    }
    

    public List<Fine> GetPendingFinesByMember(
        int memberId)
    {
        return _fineRepository
            .GetPendingFinesByMember(memberId);
    }

    

    public decimal GetPendingFineAmount(
        int memberId)
    {
        return _fineRepository
            .GetPendingFineAmount(memberId);
    }


    

    public FinePayment PayFine(
        int fineId,
        decimal amount)
    {

        var fine =
            _fineRepository.GetFineById(fineId);

        if (fine == null)
        {
            throw new Exception(
                "Fine not found");
        }

        if (fine.IsPaid == true)
        {
            throw new Exception(
                "Fine already paid");
        }

        if (amount != fine.Amount)
        {
            throw new Exception(
                "Please pay full fine amount");
        }


        
        FinePayment payment =
            new FinePayment();

        payment.FineId = fineId;

        payment.PaidAmount = amount;

        payment.PaymentDate =
            DateTime.UtcNow;



        var addedPayment =
            _fineRepository
                .AddFinePayment(payment);



  

        fine.IsPaid = true;

        _fineRepository.UpdateFine(fine);



        return addedPayment;
    }

    

    public List<FinePayment> GetFinePaymentHistory(
        int fineId)
    {
        return _fineRepository
            .GetFinePaymentHistory(fineId);
    }
}