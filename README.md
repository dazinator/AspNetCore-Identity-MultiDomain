# AspNetCore-Identity-MultiDomain
An example trying to get asp.net core identity working with seperate domains.

To replicate:

1. On windows, edit your hosts file, adding the following lines, so you will be able to browse the locally running website
on the following domains:

```
127.0.0.1	app.foo.com
127.0.0.1	app.bar.com
```

2. Start the website running. 
  Browse to `http://app.foo.com:57425/` and log in. 
  Browse to `http://app.bar.com:57426` and log in. 
  
Issue:

Want a seperate identity cookie per domain, but can't figure out how to do it!

