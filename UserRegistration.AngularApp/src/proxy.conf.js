const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:7233",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
