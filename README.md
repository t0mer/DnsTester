# DnsTester

A small Windows desktop application, written in C#, that compares how quickly two DNS servers answer the same set of lookups.

[![Dns Tester](https://img.youtube.com/vi/StBB98QeB64/0.jpg)](https://www.youtube.com/watch?v=StBB98QeB64 "Dns Tester")

## Features

* Pick two DNS servers from a built-in list, or enter two custom IPv4 addresses.
* Builds a fresh list of test host names for every run, so each run tests a different set of sites.
* Sends the same A-record queries to both servers and shows each answer and response time side by side.
* Shows a timestamped status log of the run.
* Modern "Metro" look, built with [MetroFramework](https://github.com/thielj/MetroFramework).

## How it works

1. **Collect host names.** The app makes a random three-letter search on Google (`http://www.google.com/search?num=100&q=<letters>`) and extracts host names matching `.<letters>.com` from the result page (for example `example.com` from `www.example.com`), dropping `google.com` and duplicates.
2. **Query both servers.** For each host name it sends a plain DNS A query over UDP port 53 to both servers. Queries to the first server use transaction ID `Q1`, queries to the second use `Q2`.
3. **Collect answers.** It reads replies until no packet arrives for 5 seconds. For each successful reply it shows the returned IPv4 address (or `CNAME` / `SOA`) and the elapsed time in seconds. Failed lookups (for example NXDOMAIN or SERVFAIL) are skipped and leave the row blank.

The time shown is measured from the moment the batch of queries started, not per query, so it reflects when each answer arrived during the run.

> **Note:** step 1 depends on scraping Google search results over plain HTTP. If Google changes its page or blocks the request, the status log shows either "0 Random URL's found" (the page changed) or "Host Google.com not found - are you connected ?" (the request failed). Either way, no host names are tested. <!-- TODO: verify the Google scraping still works -->

## Built-in DNS servers

The server list is loaded from [`DNSTester/Servers.xml`](DNSTester/Servers.xml), which sits next to the executable:

| Provider | Addresses |
|----------|-----------|
| Google | 8.8.8.8, 8.8.4.4 |
| Cloudflare | 1.1.1.1, 1.0.0.1 |
| Quad9 | 9.9.9.9, 149.112.112.9 |
| Verisign | 64.6.64.6, 64.6.65.6 |
| AdGuard DNS | 94.140.14.15, 94.140.15.15 |

> **Known issue:** the "Google (8.8.4.4)" entry in `Servers.xml` actually points to `8.8.8.8`.

You can add servers by adding `<server name="…" ip="…"></server>` lines to `Servers.xml`. Only IPv4 addresses are supported.

## Requirements

* Windows with .NET Framework 4.6.1 or later.
* Outbound HTTP access to `www.google.com` and outbound UDP port 53 to the DNS servers you test.

## Getting started

There are no GitHub releases. A debug build is committed in the repository at `DNSTester/bin/Debug/` (`DNSTester.exe` plus the MetroFramework DLLs and `Servers.xml`); keep those files together when you copy it.

To build it yourself:

1. Open `DNSTester.sln` in Visual Studio.
2. Restore NuGet packages if needed (MetroFramework 1.2.0.3 is referenced from the `packages/` folder).
3. Build and run the `DNSTester` project.

## Usage

1. Choose **Dns 1** and **Dns 2** from the lists, or tick **Custom Servers** and type two IPv4 addresses into **Custom Dns 1** and **Custom Dns 2**.
2. Click **Run Test**.
3. Watch the results table fill in. Its columns are **URL**, **IP Address from DNS 1**, **DNS 1 Timing**, **IP Address from DNS 2** and **DNS 2 Timing**. The status log below shows progress and errors.

## Limitations

* Only `.com` host names with a single, letters-only label before `.com` are tested (no digits or hyphens), and only when a dot comes right before them in the page.
* Queries are A records over plain UDP; DNS over HTTPS, DNS over TLS and IPv6 servers are not supported.
* The code has room for 500 unique host names (and 1000 raw matches). If a search returns more, collection fails, nothing is tested, and the log shows "Host Google.com not found - are you connected ?".
* Custom server addresses are not validated; an invalid IP address raises an unhandled error.

## Credits

* [MetroFramework](https://github.com/thielj/MetroFramework) by Jens Thiel (see [`DNSTester/MetroFramework.txt`](DNSTester/MetroFramework.txt)).

## License

[Apache License 2.0](LICENSE)
