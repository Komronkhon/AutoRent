using CarRental.Application.Abstractions;
using CarRental.Application.Features.Commands.Payments.ConfirmPayment;
using CarRental.Domain.Common;
using MediatR;

namespace CarRental.Application.Features.Commands.Payments.FailPayment
{
    public class ComfirmPaymentHandler : IRequestHandler<FailPaymentCommand, _Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComfirmPaymentHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(FailPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return "Payment not found.";

            var result = payment.Fail();

            if (result.IsFailure)
                return result.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
