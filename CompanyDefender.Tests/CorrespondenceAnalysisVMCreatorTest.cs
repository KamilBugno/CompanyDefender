using CompanyDefender.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDefender.Tests
{
    [TestClass]
    public class CorrespondenceAnalysisVMCreatorTest
    {
        [TestMethod]
        public void PersonMailGraphVMCreatorValuesTest()
        {
            var mailRecords = CreateListOfMailRecord();

            var resultFromPersonMailGraphVMCreator = new CorrespondenceAnalysisVMCreator()
                .CreateFromMailRecords(mailRecords);

            CheckMailsGraphCorrectness(resultFromPersonMailGraphVMCreator.MailsGraph);
            CheckPersonGraphCorrectness(resultFromPersonMailGraphVMCreator.PersonsGraph);
            CheckMailsTableCorrectness(resultFromPersonMailGraphVMCreator.MailsTable);
        } 

        private List<MailRecord> CreateListOfMailRecord()
        {
            var listOfMailRecord = new List<MailRecord>
            {
                new MailRecord
                {
                    mail_key = "10",
                    from = "HRSystem/1",
                    to = "HRSystem/2",
                    full_name_from = "Jan Kowalski",
                    full_name_to = "Alicja Kowalska",
                    from_mail_address = "jan.kowalski@company.com",
                    to_mail_address = "alicja.kowalska@company.com",
                    topic = "What about dinner?",
                    body = "Do you want to eat dinner with me at 12?",
                    has_attachment = "1"
                },
                new MailRecord
                {
                    mail_key = "11",
                    from = "HRSystem/1",
                    to = "HRSystem/3",
                    full_name_from = "Jan Kowalski",
                    full_name_to = "Kamil Nowak",
                    from_mail_address = "jan.kowalski@company.com",
                    to_mail_address = "kamil.nowak@company.com",
                    topic = "Spotkanie",
                    body = "Zapraszam na spotkanie zespolu o 14.00 w sali 391",
                    has_attachment = "0"
                }
            };
            return listOfMailRecord;
        }

        private void CheckMailsTableCorrectness(List<MailTableViewModel> mailsTable)
        {
            CheckFirstElementOfMailsTable(mailsTable[0]);
            CheckSecondElementOfMailsTable(mailsTable[1]);
        }

        private void CheckFirstElementOfMailsTable(MailTableViewModel mailTableViewModel)
        {
            Assert.AreEqual("10", mailTableViewModel.Id);
            Assert.AreEqual("Jan Kowalski", mailTableViewModel.FullNameFrom);
            Assert.AreEqual("Alicja Kowalska", mailTableViewModel.FullNameTo);
            Assert.AreEqual("What about dinner?", mailTableViewModel.Topic);
            Assert.AreEqual("Do you want to eat dinner with me at 12?", mailTableViewModel.Body);
            Assert.AreEqual(true, mailTableViewModel.HasAttachment);
        }

        private void CheckSecondElementOfMailsTable(MailTableViewModel mailTableViewModel)
        {
            Assert.AreEqual("11", mailTableViewModel.Id);
            Assert.AreEqual("Jan Kowalski", mailTableViewModel.FullNameFrom);
            Assert.AreEqual("Kamil Nowak", mailTableViewModel.FullNameTo);
            Assert.AreEqual("Spotkanie", mailTableViewModel.Topic);
            Assert.AreEqual("Zapraszam na spotkanie zespolu o 14.00 w sali 391", mailTableViewModel.Body);
            Assert.AreEqual(false, mailTableViewModel.HasAttachment);
        }

        private void CheckMailsGraphCorrectness(List<MailGraphViewModel> mailsGraphList)
        {
            CheckFirstElementOfMailsGraph(mailsGraphList[0]);
            CheckSecondElementOfMailsGraph(mailsGraphList[1]);
        }

        private void CheckFirstElementOfMailsGraph(MailGraphViewModel mailsGraph)
        {
            Assert.AreEqual(1, mailsGraph.From);
            Assert.AreEqual(2, mailsGraph.To);
            Assert.AreEqual("10", mailsGraph.Label);
        }

        private void CheckSecondElementOfMailsGraph(MailGraphViewModel mailsGraph)
        {
            Assert.AreEqual(1, mailsGraph.From);
            Assert.AreEqual(3, mailsGraph.To);
            Assert.AreEqual("11", mailsGraph.Label);
        }

        private void CheckPersonGraphCorrectness(List<PersonGraphViewModel> personGraphList)
        {
            CheckFirstElementOfPersonGraph(personGraphList[0]);
            CheckSecondElementOfPersonGraph(personGraphList[1]);
            CheckThirdElementOfPersonGraph(personGraphList[2]);
        }

        private void CheckFirstElementOfPersonGraph(PersonGraphViewModel personGraphViewModel)
        {
            Assert.AreEqual(1, personGraphViewModel.Id);
            Assert.AreEqual("Jan Kowalski", personGraphViewModel.Label);
        }

        private void CheckSecondElementOfPersonGraph(PersonGraphViewModel personGraphViewModel)
        {
            Assert.AreEqual(2, personGraphViewModel.Id);
            Assert.AreEqual("Alicja Kowalska", personGraphViewModel.Label);
        }

        private void CheckThirdElementOfPersonGraph(PersonGraphViewModel personGraphViewModel)
        {
            Assert.AreEqual(3, personGraphViewModel.Id);
            Assert.AreEqual("Kamil Nowak", personGraphViewModel.Label);
        }
    }
}
