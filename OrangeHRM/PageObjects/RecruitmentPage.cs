using OpenQA.Selenium;
using OrangeHRM.Common.Extensions;
using OrangeHRM.Common.WebElements;

namespace OrangeHRM.PageObjects
{
    public class RecruitmentPage : BasePage
    {
        private readonly MyWebElement EditApplicationSwitch = new(By.XPath("//span[contains(@class, 'oxd-switch-input')]/parent::*"));
        private readonly MyWebElement FirstNameTextBox = new(By.XPath("//input[@name='firstName']"));
        private readonly MyWebElement LastNameTextBox = new(By.XPath("//input[@name='lastName']"));
        private readonly MyWebElement JobVacancyDropdown = new(By.XPath("//*[contains(@class, 'oxd-select-text-input')]"));
        private readonly MyWebElement EmailTextBox = new(By.XPath("//*[text()='Email']/following::input[1]"));
        private readonly MyWebElement SubmitButton = new(By.XPath("//button[@type='submit']"));
        private readonly MyWebElement ResultMessage = new(By.XPath("//*[contains(@class, 'oxd-toast-container')]/descendant::p[1]"));
        private readonly MyWebElement FullNameInHeader = new(By.XPath("(//p[contains(@class, 'oxd-text') and ./preceding::*[contains(@class, " +
                                                                       "'orangehrm-main-title') and text()='Application Stage']])[1]"));
        private readonly MyWebElement ConfirmEditButton = new(By.XPath("//*[contains(@class, 'orangehrm-modal-footer')]" +
                                                                        "/button[text()=' Yes, Confirm ']"));

        private IReadOnlyList<IWebElement> RowsInTable =>
            Driver.FindElements(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')]]"));

        private IReadOnlyList<IWebElement> JobVacancyDropdownList => Driver.FindElements(By.XPath("//*[@role='listbox']//span"));

        public bool PageInitState()
        {
            var result = new MyWebElement(By.XPath("//h6[contains(@class, 'oxd-text') and ./parent::span]")).IsDisplayed() && 
                         new MyWebElement(By.ClassName("oxd-table")).IsDisplayed() &&
                         Driver.FindElements(By.XPath("//*[@class='oxd-table-filter']//*[contains(@class, 'oxd-grid-item')]")).Count == 9 &&
                         new MyWebElement(By.XPath("//button[text()=' Add ']")).IsDisplayed() &&
                         new MyWebElement(By.XPath("//button[@type='submit']")).IsDisplayed() &&
                         new MyWebElement(By.XPath("//button[@type='reset']")).IsDisplayed();

            return result;
        }

        public void ViewCandidateApplication(string candidateName)
        {
            candidateName = candidateName.Insert(candidateName.IndexOf(" ", StringComparison.Ordinal), " ");
            var viewButton =
                new MyWebElement(By.XPath("//*[@role='row' and ./ancestor::*[contains(@class, 'oxd-table-body')] and " +
                    $"./*[count(//*[@role='columnheader' and text()='Candidate']/preceding-sibling::*) + 1]/*[text()='{candidateName}']]" +
                    "/*/descendant::button/i[contains(@class, 'bi-eye')]"));
            viewButton.Click();
        }

        public void EditApplication(string applicantFullName, string newFullName, string email, string jobVacancy)
        {
            ViewCandidateApplication(applicantFullName);
            var newFirstName = newFullName.Split(' ')[0];
            var newLastName = newFullName.Split(' ')[1];
            EditApplicationSwitch.Click();
            Driver.GetWebDriverWait().Until(_ => FirstNameTextBox.Enabled && LastNameTextBox.Enabled);
            FirstNameTextBox.SendKeysAfterCtrlABackspace(newFirstName);
            LastNameTextBox.SendKeysAfterCtrlABackspace(newLastName);
            JobVacancyDropdown.Click();
            Driver.GetWebDriverWait().Until(_ => JobVacancyDropdownList.Count > 0);
            var vacancy = JobVacancyDropdownList.First(i => i.Text == jobVacancy);
            vacancy.Click();
            EmailTextBox.SendKeysAfterCtrlABackspace(email);
            SubmitButton.Click();
            ConfirmEditButton.Click();
        }

        public string GetSuccessMessage()
        {
            Driver.GetWebDriverWait().Until(_ => ResultMessage.IsDisplayed());
            var message = ResultMessage.Text;

            return message;
        }

        public string GetFirstNameValue() => FirstNameTextBox.GetAttribute("value");

        public string GetLastNameValue() => LastNameTextBox.GetAttribute("value");

        public string GetFullNameInHeader() => FullNameInHeader.Text;
    }
}
