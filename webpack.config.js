// webpack.config.js

const path = require('path');

module.exports = {
   // Mode configuration
  mode: 'development',

  entry: './GameHopper/wwwroot/src/index.js',
  output: {
    path: path.resolve(__dirname, 'GameHopper/wwwroot/dist'),
    filename: 'index.bundle.js',
    publicPath: '/dist/',
  },
  // Module and rules configuration
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/, // For both .js and .jsx files
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env', '@babel/preset-react'] // Add '@babel/preset-react'
          }
        },
      },
      {
        test: /\.css$/, // For CSS files
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.(png|jpg|gif|svg)$/, // For image files
        use: ['file-loader'],
      },
    ],
  },
  
  // Resolve extensions
  resolve: {
    extensions: ['.js', '.jsx'],
  },
  
  // Development server configuration
  devServer: {
    static: {
      directory: path.resolve(__dirname, 'wwwroot'),
    },
    compress: true,
    port: 3000, 
    hot: true,
    open: true,
    proxy: [
      {
        context: ['/'],
        target: 'http://localhost:5294',
        changeOrigin: true,
        secure: false,
        historyApiFallback: true,
        // bypass: function (req) {
        //   // Exclude requests for static files in '/dist'
        //   if (req.url.includes('/dist')) {
        //       return req.url; // Don't proxy, serve directly
        //   }
        // },  
      }
    ],
    
    watchFiles: ['src/**/*', 'wwwroot/**/*'],
  },
};
