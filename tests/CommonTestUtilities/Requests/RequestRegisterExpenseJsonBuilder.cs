﻿using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>().RuleFor(request => request.Title, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(request => request.Amount, faker => decimal.Parse(faker.Commerce.Price()))
            .RuleFor(request => request.Date, faker => faker.Date.Past())
            .RuleFor(request => request.PaymentType, faker => faker.PickRandom<PaymentType>());
    }
}
