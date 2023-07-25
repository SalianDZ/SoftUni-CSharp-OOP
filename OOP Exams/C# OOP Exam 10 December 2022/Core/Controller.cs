using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths = new BoothRepository();

        public string AddBooth(int capacity)
        {
            IBooth booth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(booth);
            string result = String.Format(OutputMessages.NewBoothAdded, booth.BoothId, capacity);
            return result;
        } // done

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            string result;
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            ICocktail cocktail;

            if (cocktailTypeName == "MulledWine")
            {
                if (size != "Small" && size != "Middle" && size != "Large")
                {
                    result = String.Format(OutputMessages.InvalidCocktailSize, size);
                    throw new ArgumentException(result);
                }
                cocktail = new MulledWine(cocktailName, size);
            }
            else if (cocktailTypeName == "Hibernation")
            {
                if (size != "Small" && size != "Middle" && size != "Large")
                {
                    result = String.Format(OutputMessages.InvalidCocktailSize, size);
                    throw new ArgumentException(result);
                }
                cocktail = new Hibernation(cocktailName, size);
            }
            else
            {
                result = String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
                throw new ArgumentException(result);
            }

            if (currentBooth.CocktailMenu.Models.Any(x => x.Size == size && x.Name == cocktailName))
            {
                result = String.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
                throw new ArgumentException(result);
            }

            currentBooth.CocktailMenu.AddModel(cocktail);
            result = String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
            return result;
        } // done

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            string result;
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            IDelicacy delicacy;

            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                result = String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
                throw new ArgumentException(result);
            }

            if (currentBooth.DelicacyMenu.Models.Any(x => x.Name == delicacyName))
            {
                result = String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
                throw new ArgumentException(result);
            }

            currentBooth.DelicacyMenu.AddModel(delicacy);
            result = String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);

            return result;
        } // done

        public string BoothReport(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            return currentBooth.ToString();
        } // done

        public string LeaveBooth(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format(OutputMessages.GetBill, currentBooth.CurrentBill));
            sb.AppendLine(String.Format(OutputMessages.BoothIsAvailable, boothId));
            currentBooth.Charge();
            currentBooth.ChangeStatus();
            return sb.ToString().TrimEnd();
        } // done

        public string ReserveBooth(int countOfPeople)
        {
            string result;
            IBooth wantedBooth = booths.Models
                .Where(x => x.IsReserved == false && x.Capacity >= countOfPeople)
                .OrderBy(x => x.Capacity).ThenByDescending(x => x.BoothId).FirstOrDefault();


            if (wantedBooth == null)
            {
                result = String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
                throw new ArgumentException(result);
            }

            wantedBooth.ChangeStatus();
            result = String.Format(OutputMessages.BoothReservedSuccessfully, wantedBooth.BoothId, countOfPeople);
            return result;
        } // done

        public string TryOrder(int boothId, string order)
        {
            string[] orderDetails = order.Split("/");
            string itemTypeName = orderDetails[0];
            string itemName = orderDetails[1];
            int count = int.Parse(orderDetails[2]);
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            string result;

            if (itemTypeName != "MulledWine" && itemTypeName != "Hibernation"
                && itemTypeName != "Gingerbread" && itemTypeName != "Stolen")
            {
                result = String.Format(OutputMessages.NotRecognizedType, itemTypeName);
                throw new ArgumentException(result);
            }

            if (!currentBooth.CocktailMenu.Models.Any(x => x.Name == itemName) && !currentBooth.DelicacyMenu.Models.Any(x => x.Name == itemName))
            {
                result = String.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                throw new ArgumentException(result);
            }

            if (orderDetails.Length == 4)
            {
                ICocktail cocktail;
                string size = orderDetails[3];
                if (itemTypeName == "MulledWine")
                {
                    cocktail = currentBooth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName && x.Size == size);

                    if (cocktail == null)
                    {
                        result = String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                        throw new ArgumentException(result);
                    }

                    currentBooth.UpdateCurrentBill(cocktail.Price * count);
                }
                else if (itemTypeName == "Hibernation")
                {
                    cocktail = currentBooth.CocktailMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName && x.Size == size);

                    if (cocktail == null)
                    {
                        result = String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                        throw new ArgumentException(result);
                    }

                    currentBooth.UpdateCurrentBill(cocktail.Price * count);
                }
                //else
                //{
                //    result = String.Format(OutputMessages.NotRecognizedType, itemTypeName);
                //    throw new ArgumentException(result);
                //}
            }
            else if (orderDetails.Length == 3)
            {
                IDelicacy delicacy;

                if (itemTypeName == "Gingerbread")
                {
                    delicacy = currentBooth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName);

                    if (delicacy == null)
                    {
                        result = String.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                        throw new ArgumentException(result);
                    }

                    currentBooth.UpdateCurrentBill(delicacy.Price * count);
                }
                else if (itemTypeName == "Stolen")
                {
                    delicacy = currentBooth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == itemName && x.GetType().Name == itemTypeName);

                    if (delicacy == null)
                    {
                        result = String.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                        throw new ArgumentException(result);
                    }

                    currentBooth.UpdateCurrentBill(delicacy.Price * count);
                }
                //else
                //{
                //    result = String.Format(OutputMessages.NotRecognizedType, itemTypeName);
                //    throw new ArgumentException(result);
                //}
            }

            result = String.Format(OutputMessages.SuccessfullyOrdered, currentBooth.BoothId, count, itemName);
            return result;
        } // done
    }
}
