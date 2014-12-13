module internal SolutionTree.SampleSolutionFiles

    
    [<Literal>]
    let ``17 items, 3 levels`` =
        """
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 2013
VisualStudioVersion = 12.0.31101.0
MinimumVisualStudioVersion = 10.0.40219.1
Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "Sample.Library1", "Sample.Library1\Sample.Library1.fsproj", "{E4B18A3F-2B9C-46AD-A242-27123D687693}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Solution items", "Solution items", "{80B48F46-49C1-483A-A0AB-FC232CCD3395}"
	ProjectSection(SolutionItems) = preProject
		Readme.txt = Readme.txt
	EndProjectSection
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Solution items 2", "Solution items 2", "{F2D91C00-76DC-4CA4-9684-29FAB3EE0810}"
	ProjectSection(SolutionItems) = preProject
		Script1.fsx = Script1.fsx
	EndProjectSection
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Src", "Src", "{5C2EBC10-A7F0-4DF0-B044-CC9982A49A27}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Tests", "Tests", "{9F44CA6F-4A39-4862-8377-50F866CB3C2C}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Server", "Server", "{D672BF9C-E983-46C8-B045-F972FD4BCA41}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Client", "Client", "{4C995720-F756-43F1-BD78-0C5D524DF172}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Client", "Client", "{69E4F0CC-ED4E-4BEB-BD3B-AD0FBEB3AA5A}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Server", "Server", "{8F54D638-E7D2-4F61-AD5A-31A04E3501AE}"
EndProject
Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "Server", "Src\Server\Server\Server.fsproj", "{41CDB151-73F8-4241-B18A-E92B5D5D312A}"
EndProject
Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "Client.Web", "Src\Client\Client.Web\Client.Web.fsproj", "{CC244D9A-7F1F-4EDA-BA09-8CCB3A4327EC}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = ".nuget", ".nuget", "{59F4629C-1F70-471C-932D-D05129057360}"
	ProjectSection(SolutionItems) = preProject
		.nuget\packages.config = .nuget\packages.config
	EndProjectSection
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Client.Tests", "Tests\Client\Client.Tests\Client.Tests.csproj", "{A23944E7-1D7F-4641-A17B-670654A523E8}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Server.ConsoleApplication", "Server.ConsoleApplication\Server.ConsoleApplication.csproj", "{C1AA2F5B-CA48-4DB1-A66F-80E90A078082}"
EndProject
Global
	GlobalSection(NestedProjects) = preSolution
		{D672BF9C-E983-46C8-B045-F972FD4BCA41} = {5C2EBC10-A7F0-4DF0-B044-CC9982A49A27}
		{4C995720-F756-43F1-BD78-0C5D524DF172} = {5C2EBC10-A7F0-4DF0-B044-CC9982A49A27}
		{69E4F0CC-ED4E-4BEB-BD3B-AD0FBEB3AA5A} = {9F44CA6F-4A39-4862-8377-50F866CB3C2C}
		{8F54D638-E7D2-4F61-AD5A-31A04E3501AE} = {9F44CA6F-4A39-4862-8377-50F866CB3C2C}
		{41CDB151-73F8-4241-B18A-E92B5D5D312A} = {D672BF9C-E983-46C8-B045-F972FD4BCA41}
		{CC244D9A-7F1F-4EDA-BA09-8CCB3A4327EC} = {4C995720-F756-43F1-BD78-0C5D524DF172}
		{59F4629C-1F70-471C-932D-D05129057360} = {4C995720-F756-43F1-BD78-0C5D524DF172}
		{A23944E7-1D7F-4641-A17B-670654A523E8} = {69E4F0CC-ED4E-4BEB-BD3B-AD0FBEB3AA5A}
		{C1AA2F5B-CA48-4DB1-A66F-80E90A078082} = {D672BF9C-E983-46C8-B045-F972FD4BCA41}
	EndGlobalSection
EndGlobal
"""