#import <UIKit/UIKit.h>

extern "C" {
    void CustomOpenUrl(const char* url) {
        if (url == NULL) {
            return;
        }

        NSString *nsUrlString = [NSString stringWithUTF8String:url];
        NSURL *nsUrl = [NSURL URLWithString:nsUrlString];

        if (nsUrl == nil) {
            return;
        }

        dispatch_async(dispatch_get_main_queue(), ^{
            if (@available(iOS 10.0, *)) {
                [[UIApplication sharedApplication] openURL:nsUrl options:@{} completionHandler:nil];
            } else {
                [[UIApplication sharedApplication] openURL:nsUrl];
            }
        });
    }
}
