// <copyright file="WebDriverUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// Helper class that will keep repetitive functions necessary for all kinds of operations such as interacting with web elements and so on.
    /// </summary>
    public static class WebDriverUtils
    {
        /// <summary>
        /// Helper method that interacts with an input web element.
        /// </summary>
        /// <param name="actions">The Actions class which provides mechanism for advanced interaction with the browser.</param>
        /// <param name="inputElement">The input element to be interacted with.</param>
        /// <param name="inputElementText">The input element text that should be inserted as value in the input field itself.</param>
        public static void InteractWithInputElement(Actions actions, IWebElement inputElement, string inputElementText)
        {
            actions
                .Click(inputElement)
                .Pause(TimeSpan.FromSeconds(2))
                .SendKeys(inputElementText)
                .Perform();
        }

        /// <summary>
        /// Helper method that interacts with submit buttons on the webpage.
        /// </summary>
        /// <param name="actions">The Actions class which provides mechanism for advanced interaction with the browser.</param>
        /// <param name="submitButton">The submit button which will be interacted with.</param>
        public static void InteractWithSubmitButton(Actions actions, IWebElement submitButton)
        {
            actions
                .Click(submitButton)
                .Pause(TimeSpan.FromSeconds(2))
                .Perform();
        }
    }
}
