# Web API and Azure AD lab

For this lab, imagine we have a dependency between two components which
previously have been part of the a monolith MVC-application with a bespoke
authentication and authorization layer. We want to a clear separation between
frontend and backend--perhaps because an anticipated need for a mobile client.
As such, authentication and authorization is moved to Azure AD and our
challenges are as follows:

1. Using Azure AD to authenticate the user
2. Present some proof of identity to the backend __or__ allow the backend to
   impersonate the user
3. Verify the proof of identity provided __or__ retrieve token used to
   impersonate the user
4. Be authorized by the backend to consume some resource belonging to the user

## Details

For this lab, we have a Web API backend which responds to the MVC-frontend's
queries regarding orders placed by a certain user identified by an user id.

- Order ids are randomly generated based on the provided user id
- User ids are not validated and need not be real

## Scenarios to explore

- Decide on how to achieve the above using the scenarios described at:
  http://msdn.microsoft.com/library/azure/jj573266.aspx
- What protocol? SAML Passive, OAuth, or WS-Federation?
- What are the distinctions between the two different application types we
  can create for an Azure AD tenant?
- If needed, how can we retrieve more information about the user than provided
  by the claims?
- How would we implement role-based authorization?
- Is it necessary establish some kind of trust between frontend and backend?
- How would we establish trust between two components?
- Imagine a "real" data persistence layer, how can we ensure tenants are only
  allowed to access their own data? What are the implications and trade-offs?

## Quirks and pitfalls

- http://www.cloudidentity.com/blog/2013/12/11/setting-up-an-asp-net-project-with-organizational-authentication-requires-an-organizational-account/
- Avoid ACS as it is no longer being developed (e.g. ADAL 2.0 will not support ACS)
- Avoid AAL as it has been superseded by ADAL
- Avoid manually editing the application manifest as it will break the portal :(
- "App ID URI" is just unique identifier for your application

## Resources

### Concept overview
- https://cwiki.apache.org/confluence/download/attachments/27849062/Fediz_Detailed.png?version=1&modificationDate=1339179408000&api=v2

### Securing Web API
- http://msdn.microsoft.com/en-us/magazine/dn463788.aspx
- http://www.cloudidentity.com/blog/2013/07/23/securing-a-web-api-with-windows-azure-ad-and-katana/
- http://www.cloudidentity.com/blog/2013/12/10/protecting-a-self-hosted-api-with-microsoft-owin-security-activedirectory/

### ADAL
- http://www.cloudidentity.com/blog/2013/09/12/active-directory-authentication-library-adal-v1-for-net-general-availability/
- http://www.cloudidentity.com/blog/2013/09/16/getting-acquainted-with-authenticationresult/
- http://www.cloudidentity.com/blog/2013/10/29/using-adals-acquiretokenby-authorizationcode-to-call-a-web-api-from-a-web-app/
