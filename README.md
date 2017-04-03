netTest
======

## About
I created netTest so my mom could diagnose her own internet problems.  I
noticed that I was always checking the same things in the same order and making
the same conclusions when I found something didn't work.  

## Usage
Get the latest binary from the releases section of my
[github project](https://github.com/rigel314/netTest). You should be able to
run the exe, provided you have .NET Framework 4.5.  When it opens, it will
immediately start the test.  The tests shouldn't take longer than 20 seconds.
It will then tell you what worked, didn't work, and something that might fix
it.

### What your less technically inclined people need to know
 * Make sure your people know which box is the router.
 * Make sure your people know whether they have a modem or not.
 * Make sure your people know which box is the modem.
 * Make sure your people know how to reboot their router/modem.
 * Make sure your people know whether they connect with WiFi or Ethernet.
 * Make sure your people's router has an internal web interface on port 80.

## Build
I'm using Visual Studio 2013.  My build process depends on the Costura.Fody
NuGet plugin to bundle a dependent dll into the exe.

## The Debug Sequence
 1. I check that you have a network interface that is up.
    a. if it fails, I can't go further and tell you to reconnect/reboot your router.
 2. I find your default gateway and try to connect on port 80
    a. If it fails, I can't go further and tell you to reboot the router.
 3. I try to connect to port 53 of 8.8.8.8 (Google's DNS server)
    a. If it fails, I can't go further and tell you to reboot your modem.  Or maybe your router.  But it seems like the ISP is really having a problem.
 4. I try resolving google.com at your default DNS server.
    a. If it fails, I check against 8.8.8.8
       i. if it fails, that's totally weird.  Try rebooting your computer/router.  Maybe there's a firewall blocking DNS?
       ii. If it worked, your ISP DNS servers are down.  You can wait for them to come up or you can switch to OpenDNS http://208.69.38.205/
