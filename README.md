# TurnTable
A single- and multi-player game website.

### To do
- SignalR hub should just be used to notify the front end that that game state has changed.
- When that happens, the front end should hit a GET /api/game/AAAA which gets the game state for game AAAA.
- In the GameDTO will be: type, code, player number for who's turn it is, is game over, winner player number, array of players, game state I.e. current game board
-Then when someone joins the backend will send to User 1 "game state changed" and since there is a second player the game can begin.
- This has a few extra benefits besides simplifying SignalR code. If there is ever an error, we can just re-get the game state. If we store the game code in storage or URL, the game board can be got when the user navigates to the page again