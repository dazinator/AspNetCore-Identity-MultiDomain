# AspNetCore-Identity-MultiDomain
An example trying to get asp.net core identity cookie, shared for a domain. 

To replicate the problem:

1. On windows, edit your hosts file, adding the following lines, so you will be able to browse the locally running website
on the following domains:

```
127.0.0.1	app.foo.com
127.0.0.1	app.bar.com
127.0.0.1	another.bar.com
```

When you log in, we want the asp.net identity cookie to be shared accross `.bar.com` - so in other words, `.bar.com` and `.foo.com` should be segregated.

To replicate the issue:

2. Start the website running. 
3. Browse to `http://app.bar.com:57426/` and register a user account. (It will ask you to apply the EF migrations during this)
4. Once logged in, browse to `http://another.bar.com:57427/` - we want you to still be logged in.

Issue:
Cannot figure out how to segregate the identity cookie for a domain.

