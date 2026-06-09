using CarRental.Application.Abstractions;
using CarRental.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Features.Commands.Payments.DeletePayment
{
    public sealed class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, _Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePaymentHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<_Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByGuidAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return "User not found";

            payment.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _Result.Success();
        }
    }
}
