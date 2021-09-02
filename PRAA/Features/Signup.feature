Feature: Signup
Simple signup for Kahoot

Link to a feature: [Calculator](PRAA/Features/Signup.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Signup for a Kahoot Account
	Given You are a student
	When you successfully signup with your email address 
	Then you login to your kahoot account