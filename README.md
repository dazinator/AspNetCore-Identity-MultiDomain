# AspNetCore-Identity-MultiDomain
An example trying to get asp.net core identity working with seperate domains.

To replicate the problem:

1. On windows, edit your hosts file, adding the following lines, so you will be able to browse the locally running website
on the following domains:

```
127.0.0.1	app.foo.com
127.0.0.1	app.bar.com
```

2. Start the website running. 
3. Browse to `http://app.foo.com:57425/` and register a user account. (It will ask you to apply the EF migrations during this)
4. Clear your cookies. Try logging in and out using your account on the various domains:
  - `http://app.bar.com:57426/`
  - `http://app.foo.com:57425/`
  - `http://localhsot:5000/`
4. On some domains, it will appear to login, but it won't issue the identity cookie, and so app still sees you as logged out.

  
Issue:
On some domains, it will appear to login, but it won't issue the identity cookie, and so app still sees you as logged out.
How do we set up identity cookies per domain?

