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
- SAML, OAuth, or WS-Federation?
- What are the distinctions between the two different application we can create
  for an Azure AD?
- If needed, how can we retrieve more information about the user than provided
  by the claims?
- Is it necessary establish some kind of trust between frontend and backend?

## Pitfalls and things to avoid

- http://www.cloudidentity.com/blog/2013/12/11/setting-up-an-asp-net-project-with-organizational-authentication-requires-an-organizational-account/
- ACS as it is no longer being developed
- AAL which has been superseded by ADAL
- Manually editing the application manifest

## Resources

### Securing Web API
- http://msdn.microsoft.com/en-us/magazine/dn463788.aspx
- http://www.cloudidentity.com/blog/2013/07/23/securing-a-web-api-with-windows-azure-ad-and-katana/
- http://www.cloudidentity.com/blog/2013/12/10/protecting-a-self-hosted-api-with-microsoft-owin-security-activedirectory/

### ADAL
- http://www.cloudidentity.com/blog/2013/09/12/active-directory-authentication-library-adal-v1-for-net-general-availability/
- http://www.cloudidentity.com/blog/2013/09/16/getting-acquainted-with-authenticationresult/
- http://www.cloudidentity.com/blog/2013/10/29/using-adals-acquiretokenby-authorizationcode-to-call-a-web-api-from-a-web-app/
- http://www.cloudidentity.com/blog/2013/10/29/using-adals-acquiretokenby-authorizationcode-to-call-a-web-api-from-a-web-app/

