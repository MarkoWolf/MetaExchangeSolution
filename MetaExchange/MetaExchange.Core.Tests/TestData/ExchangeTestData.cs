using MetaExchange.Core.Models;

namespace MetaExchange.Core.Tests.TestData;

public static class ExchangeTestData
{
    public static List<Exchange> GetExchanges()
    {
        return new List<Exchange>
        {
            new Exchange
            {
                Id = "exchange-01",
                AvailableFunds = new Funds
                {
                    Crypto = 10.8503m,
                    Euro = 117520.12m
                },
                OrderBook = new OrderBook
                {
                    Bids = new List<OrderEntry>
                    {
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("6e9fe255-a776-4965-9bf4-9f076361f5cb"),
                                Time = DateTime.Parse("2024-03-01T14:41:06.563Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.01m,
                                Price = 57226.46m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("86b69db0-b1cb-49d5-beb7-7068cfcbe14d"),
                                Time = DateTime.Parse("2024-03-01T21:11:09.439Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.5m,
                                Price = 57226.08m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("a56ef59d-c75d-491f-a972-7ea302894ed4"),
                                Time = DateTime.Parse("2024-03-01T15:45:53.288Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.062m,
                                Price = 57225.5m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("3b697325-666b-4a6c-9267-c5551895deb2"),
                                Time = DateTime.Parse("2024-03-01T08:16:48.415Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 4m,
                                Price = 57211m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("c37f93d1-6d87-4759-af26-2d0d0ad09073"),
                                Time = DateTime.Parse("2024-03-01T17:29:35.568Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.02426084m,
                                Price = 57210.81m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("0dbf6715-a864-41a3-978e-06e378a2cb2b"),
                                Time = DateTime.Parse("2024-03-01T12:45:25.228Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.5m,
                                Price = 57194.96m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("c5a53b44-c1fa-476c-b450-d995d08a71ad"),
                                Time = DateTime.Parse("2024-03-01T11:37:56.308Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.43m,
                                Price = 57176.98m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("8ce91257-dafd-41eb-a655-b2d23fc63eeb"),
                                Time = DateTime.Parse("2024-03-01T03:28:44.618Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 4.824m,
                                Price = 57173.5m
                            }
                        }
                    },
                    Asks = new List<OrderEntry>
                    {
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("719f85c9-163e-471c-8edd-67021cfef195"),
                                Time = DateTime.Parse("2024-03-01T00:46:50.389Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57299.73m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("4d8d9414-399a-4ceb-8d09-2af7e4db6000"),
                                Time = DateTime.Parse("2024-03-01T09:46:16.308Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57299.92m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("aaad94ec-8a3e-41b6-b9b8-1a3e88670531"),
                                Time = DateTime.Parse("2024-03-01T14:40:50.905Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.49m,
                                Price = 57313.45m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("8b78555a-9920-49a9-b250-a9a34ce89f35"),
                                Time = DateTime.Parse("2024-03-01T19:25:16.060Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 1.6m,
                                Price = 57340.13m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("9fabb15b-4bf8-44fa-8ef7-17df5ff2ff19"),
                                Time = DateTime.Parse("2024-03-01T01:59:42.702Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57340.9m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("6ecad696-a8f7-4fe5-95a5-77550f94c5aa"),
                                Time = DateTime.Parse("2024-03-01T22:34:03.949Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.08971m,
                                Price = 57372.79m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("0e810986-cdbf-4ffc-b8dc-f55cbe55e61e"),
                                Time = DateTime.Parse("2024-03-01T10:23:17.267Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.01m,
                                Price = 57372.99m
                            }
                        }
                    }
                }
            },
            new Exchange
            {
                Id = "exchange-02",
                AvailableFunds = new Funds
                {
                    Crypto = 1.1821m,
                    Euro = 59435.00m
                },
                OrderBook = new OrderBook
                {
                    Bids = new List<OrderEntry>
                    {
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("e770ad2e-ef95-407a-94c5-8ad950b26059"),
                                Time = DateTime.Parse("2024-03-01T22:26:03.464Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.01m,
                                Price = 57226.46m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("c69daeae-e354-47b0-a1ef-3038664a8dd7"),
                                Time = DateTime.Parse("2024-03-01T07:27:08.258Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.5m,
                                Price = 57226.08m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("4812e720-6b73-419f-9875-1f677de0bf29"),
                                Time = DateTime.Parse("2024-03-01T07:31:01.261Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.062m,
                                Price = 57225.5m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("732557cd-db23-4792-af94-e2edbc00a9b6"),
                                Time = DateTime.Parse("2024-03-01T15:17:21.810Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 4m,
                                Price = 57211m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("df07a2ba-98f3-4ef4-a679-4333d9ee65dd"),
                                Time = DateTime.Parse("2024-03-01T13:58:46.766Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.02426084m,
                                Price = 57210.81m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("31fe932e-140f-4e82-bde7-4d4fae3f8379"),
                                Time = DateTime.Parse("2024-03-01T13:14:43.665Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.5m,
                                Price = 57194.96m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("cd2ce0e5-d349-4248-8099-a0d2b0c54148"),
                                Time = DateTime.Parse("2024-03-01T17:11:50.667Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.43m,
                                Price = 57176.98m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("11f23995-31e7-48a0-b0b7-c29ce81ae0fa"),
                                Time = DateTime.Parse("2024-03-01T04:20:24.588Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 4.824m,
                                Price = 57173.5m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("3af49440-3a59-427c-8cc6-4a5a4d5c57e5"),
                                Time = DateTime.Parse("2024-03-01T05:15:56.026Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.62m,
                                Price = 57170.41m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("7870085b-fb62-4597-a4d6-590a679fe22a"),
                                Time = DateTime.Parse("2024-03-01T12:44:25.368Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 13m,
                                Price = 57159.58m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("35100f14-ba0c-4d90-98c9-05a5a7acbbbf"),
                                Time = DateTime.Parse("2024-03-01T20:12:05.125Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.01226619m,
                                Price = 57159.2m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("eff78dfd-d52a-49ef-a2c9-bb95d4ae55e2"),
                                Time = DateTime.Parse("2024-03-01T03:11:59.519Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 4.678m,
                                Price = 57151.66m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("c448fdd4-a31c-4aef-8f8e-5f24899ed402"),
                                Time = DateTime.Parse("2024-03-01T18:20:31.947Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.00473008m,
                                Price = 57149.72m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("31c4e38c-a84b-4eba-843d-33463ffbe7bc"),
                                Time = DateTime.Parse("2024-03-01T03:20:08.345Z"),
                                Type = "Buy",
                                Kind = "Limit",
                                Amount = 0.43m,
                                Price = 57148.57m
                            }
                        }
                    },
                    Asks = new List<OrderEntry>
                    {
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("ff9ec95f-6829-4ac1-ac57-21e0b9dbdc6d"),
                                Time = DateTime.Parse("2024-03-01T22:14:09.024Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57299.73m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("3f4dd844-2a32-40fe-a18e-b7fdc6cbd5ee"),
                                Time = DateTime.Parse("2024-03-01T20:08:03.812Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57299.92m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("b0cb1bfc-f160-4eb8-9d77-3ea3cc0eb154"),
                                Time = DateTime.Parse("2024-03-01T00:04:57.663Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.49m,
                                Price = 57313.45m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("5eeedd1e-aa4e-471e-8788-3f157bdd99e9"),
                                Time = DateTime.Parse("2024-03-01T13:46:27.031Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 1.6m,
                                Price = 57340.13m
                            }
                        },
                        new OrderEntry
                        {
                            Order = new Order
                            {
                                Id = Guid.Parse("7698499f-6aa0-4f34-979f-f0c2564e5e21"),
                                Time = DateTime.Parse("2024-03-01T10:26:19.504Z"),
                                Type = "Sell",
                                Kind = "Limit",
                                Amount = 0.405m,
                                Price = 57340.9m
                            }
                        }
                    }
                }
            }
        };
    }
}