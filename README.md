# Screen-Stremer-App
This is a C# application used to stream your own desktop screen or to watch someone else one.
Streamer side: the application captures the desktop screen, splits the image in series of bytes and when a client connects to the host, it sends this series separately (the streamer is always listening on the known port)
Client side: when it connects to the streamer it receives the data and assembles the byte to get the image(part of the video)
